import { JobsIndexRepository, JobDoc } from './repositories';

export class SearchService {
    constructor(private repo: JobsIndexRepository) {}

    upsert(job: JobDoc) { return this.repo.upsert(job); }
    remove(id: string) { return this.repo.delete(id); }
    search(q?: string, industry?: string, minSalary?: number, maxSalary?: number, minExp?: number) {
        return this.repo.search({ q, industry, minSalary, maxSalary, minExp });
    }
}
