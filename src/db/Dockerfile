FROM postgres:16.1-alpine

COPY ./init-db.sql /docker-entrypoint-initdb.d/init-db.sql

EXPOSE 5432
CMD ["postgres", "-c", "log_statement=all"]