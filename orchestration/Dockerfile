# Multistage part 1: Builder
FROM microsoft/dotnet:2-sdk AS builder
EXPOSE 80

WORKDIR /build

COPY orchestration.fsproj .
RUN dotnet restore 

COPY *.fs /

RUN dotnet publish -c Release -o /build/out

# Multistage part 2: Runtime
FROM microsoft/dotnet:2.0.0-runtime-jessie

WORKDIR /app

COPY --from=builder /build/out .
ENTRYPOINT ["dotnet", "orchestration.dll"]
