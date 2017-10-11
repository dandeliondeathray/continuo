FROM debian:stretch

RUN apt-get update
RUN apt-get install -y curl
RUN groupadd -r ngrok && \
    useradd --no-log-init -r -g ngrok ngrok
USER ngrok
WORKDIR /home/ngrok

COPY ngrok .
EXPOSE 4040

ENTRYPOINT ["./ngrok", "start", "--config", "./ngrok-config.yml"]

COPY ngrok-config.yml .
