import Fastify from 'fastify';
import * as dotenv from "dotenv";
import { init } from './db';
import { JobsIndexRepository } from './repositories';
import { SearchService } from './services';
import { JobEvent } from './types';

dotenv.config();
const PORT = Number(process.env.PORT || 8005);

async function main() {
    await init();
    const app = Fastify();
    const service = new SearchService(new JobsIndexRepository());

    app.post('/index/jobs', async (req, reply) => {
        const body = req.body as JobEvent;
        if (!body?.Id) return reply.code(400).send({ error: 'Invalid payload' });
        await service.upsert(body);
        return reply.code(202).send({ ok: true });
    });

    app.delete('/index/jobs/:id', async (req, reply) => {
        const id = (req.params as any).id as string;
        await service.remove(id);
        return reply.code(202).send({ ok: true });
    });

    app.get('/search/jobs', async (req, reply) => {
        const q = (req.query as any).q as string | undefined;
        const industry = (req.query as any).industry as string | undefined;
        const minSalary = Number((req.query as any).minSalary ?? 0) || undefined;
        const maxSalary = Number((req.query as any).maxSalary ?? 0) || undefined;
        const minExp = Number((req.query as any).minExp ?? 0) || undefined;

        const result = await service.search(q, industry, minSalary, maxSalary, minExp);
        return reply.send(result);
    });

    await app.listen({ port: PORT, host: '0.0.0.0' });
    console.log(`search-service listening on :${PORT}`);
}

main().catch((e) => { console.error(e); process.exit(1); });
