using System;
using FluentMigrator;

namespace PMB.Migrations.Migration
{
    [Migration(3)]
    public class AddPgTypes: FluentMigrator.Migration
    {
        public override void Up()
        {
            const string sql = @"

            DROP TYPE IF EXISTS bet_dal_v1;

            DO
            $$
                BEGIN
                    IF NOT EXISTS(SELECT 1 FROM pg_type WHERE typname = 'bet_dal_v1') THEN
                        CREATE TYPE bet_dal_v1 AS
                        (
                            id bigint,
                            bookmaker text,
                            coefficient numeric,
                            direction text,
                            bet_type text,
                            sport text,
                            parameter numeric,
                            bet_value text,
                            ev_id text,
                            other_data text,
                            teams text,
                            url text,
                            forks_count int,
                            bet_id text,
                            is_req boolean,
                            match_data text,
                            is_initiator boolean
                        );
                    END IF;
                END
            $$;

            DROP TYPE IF EXISTS fork_dal_v1;

            DO
            $$
                BEGIN
                    IF NOT EXISTS(SELECT 1 FROM pg_type WHERE typname = 'fork_dal_v1') THEN
                        CREATE TYPE fork_dal_v1 AS
                        (
                            id bigint,
                            fork_id bigint,
                            update_count int,
                            lifetime bigint,
                            profit numeric,
                            sport text,
                            bet_type text,
                            bookmakers text,
                            teams text,
                            other text,
                            created_at timestamp with time zone,
                            first_bet_id bigint,
                            second_bet_id bigint,
                            crid_id text,
                            k1 text,
                            k2 text,
                            el_id text
                        );
                    END IF;
                END
            $$;
            ";

            Execute.Sql(sql);
        }

        public override void Down()
        {
            throw new NotImplementedException("Only up");
        }
    }
}