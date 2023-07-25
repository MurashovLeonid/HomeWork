-- Table: public.EntryRows

-- DROP TABLE IF EXISTS public."EntryRows";

CREATE TABLE IF NOT EXISTS public."EntryRows"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "Added" timestamp with time zone NOT NULL,
    "Deleted" timestamp with time zone,
    "Information" text COLLATE pg_catalog."default",
    CONSTRAINT "PK_EntryRows" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."EntryRows"
    OWNER to postgres;




 -- Table: public.TaskRows

-- DROP TABLE IF EXISTS public."TaskRows";

CREATE TABLE IF NOT EXISTS public."TaskRows"
(
    "Id" integer NOT NULL GENERATED BY DEFAULT AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1 ),
    "IdSource" integer NOT NULL,
    "Information" text COLLATE pg_catalog."default",
    "Added" timestamp with time zone NOT NULL,
    "Completed" timestamp with time zone,
    "TypeWork" integer NOT NULL,
    CONSTRAINT "PK_TaskRows" PRIMARY KEY ("Id")
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."TaskRows"
    OWNER to postgres;