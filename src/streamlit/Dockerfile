FROM python:3.12-slim
WORKDIR /app

# Install expat for kaleido package
RUN apt-get update && apt-get install -y libexpat-dev

ADD requirements.txt .
RUN pip install --no-cache-dir -r requirements.txt

ADD src src/.

ENV PORT=8501
EXPOSE ${PORT}
ENV DEBUG=0
CMD $(if [ ${DEBUG} -eq 1 ]; then echo python -m debugpy --listen 0.0.0.0:9001 -m; fi) \
    streamlit run src/app.py --server.port ${PORT}
