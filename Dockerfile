FROM python:3.12-slim AS build
WORKDIR /app

ADD requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

ADD src src/.

FROM build AS runtime
ENV PORT=8501
EXPOSE ${PORT}
ENV DEBUG=0
CMD $(if [ ${DEBUG} -eq 1 ]; then echo python -m debugpy --listen 0.0.0.0:9001 -m; fi) \
    streamlit run src/app.py --server.port ${PORT}
