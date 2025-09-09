import { pool } from './db';

export type JobDoc = {
    Id: string;
    EmployerId: string;
    Title: string;
    Description: string;
    Requirements: string;
    SalaryMin: number;
    SalaryMax: number;
    ExperienceRequired: number;
    Industry: string;
};

export class JobsIndexRepository {
    async upsert(job: JobDoc) {
        await pool.query(
            `insert into jobs_index (id, employer_id, title, description, requirements, salary_min, salary_max, experience_required, industry)
       values ($1,$2,$3,$4,$5,$6,$7,$8,$9)
       on conflict (id) do update set
         employer_id=$2, title=$3, description=$4, requirements=$5,
         salary_min=$6, salary_max=$7, experience_required=$8, industry=$9`,
            [job.Id, job.EmployerId, job.Title, job.Description, job.Requirements, job.SalaryMin, job.SalaryMax, job.ExperienceRequired, job.Industry]
        );
    }

    async delete(id: string) {
        await pool.query(`delete from jobs_index where id=$1`, [id]);
    }

    async search(opts: { q?: string; industry?: string; minSalary?: number; maxSalary?: number; minExp?: number }) {
        const clauses: string[] = [];
        const params: any[] = [];
        const push = (sql: string, val: any) => { params.push(val); clauses.push(sql.replace('$', `$${params.length}`)); };

        if (opts.q) {
            push(`(title ilike '%'||$||'%' or description ilike '%'||$||'%' or requirements ilike '%'||$||'%')`, opts.q);
        }
        if (opts.industry) push(`industry = $`, opts.industry);
        if (opts.minSalary) push(`salary_max >= $`, opts.minSalary);
        if (opts.maxSalary) push(`salary_min <= $`, opts.maxSalary);
        if (opts.minExp) push(`experience_required >= $`, opts.minExp);

        const where = clauses.length ? `where ${clauses.join(' and ')}` : '';
        const sql = `select * from jobs_index ${where} order by created_at desc limit 100`;
        const res = await pool.query(sql, params);
        return res.rows;
    }
}
