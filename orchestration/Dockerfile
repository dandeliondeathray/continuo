# Multistage part 1: Builder
FROM microsoft/dotnet:2-sdk AS builder
RUN dotnet --version
WORKDIR /build

COPY src/Continuo.Orchestration/Continuo.Orchestration.fsproj ./src/Continuo.Orchestration/
COPY test/Continuo.Orchestration.Tests/Continuo.Orchestration.Tests.fsproj ./test/Continuo.Orchestration.Tests/
COPY *.sln .
RUN dotnet restore 

COPY src/Continuo.Orchestration/*.fs src/Continuo.Orchestration/
COPY test/Continuo.Orchestration.Tests/*.fs test/Continuo.Orchestration.Tests/
RUN dotnet test test/Continuo.Orchestration.Tests

RUN dotnet publish src/Continuo.Orchestration -c Release -o /build/out

# Multistage part 2: Runtime
FROM microsoft/dotnet:2-runtime

WORKDIR /app

COPY --from=builder /build/out .

CMD ["dotnet", "Continuo.Orchestration.dll"]
