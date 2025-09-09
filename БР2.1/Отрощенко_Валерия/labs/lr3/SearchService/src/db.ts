import { Pool } from 'pg';
import * as dotenv from "dotenv";
dotenv.config();

export const pool = new Pool({ connectionString: process.env.DATABASE_URL });

export async function init() {
    await pool.query(`
    create extension if not exists "uuid-ossp";
    create table if not exists jobs_index (
      id uuid primary key default uuid_generate_v4(),
      employer_id uuid not null,
      title text not null,
      description text not null,
      requirements text not null,
      salary_min int not null,
      salary_max int not null,
      experience_required int not null,
      industry varchar(100) not null,
      created_at timestamptz not null default now()
    );
    create index if not exists idx_jobs_index_created_at on jobs_index(created_at desc);
  `);
}
