FROM alpine:latest
RUN apk add --update jq curl

WORKDIR /opener

ENTRYPOINT ["./open-tunnel.sh"]

COPY *.sh ./
