# Usa a imagem base do SDK do .NET
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia o arquivo csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código-fonte restante
COPY . ./

# Publica o aplicativo
RUN dotnet publish -c Release -o out

# Estágio de criação da imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Define o diretório de trabalho dentro do contêiner
WORKDIR /app

# Copia o resultado da compilação do estágio de compilação para o estágio de execução
COPY --from=build-env /app/out .

# Install the agent
RUN apt-get update && apt-get install -y wget ca-certificates gnupg \
&& echo 'deb http://apt.newrelic.com/debian/ newrelic non-free' | tee /etc/apt/sources.list.d/newrelic.list \
&& wget https://download.newrelic.com/548C16BF.gpg \
&& apt-key add 548C16BF.gpg \
&& apt-get update \
&& apt-get install -y 'newrelic-dotnet-agent' \
&& rm -rf /var/lib/apt/lists/*

# Enable the agent
ENV CORECLR_ENABLE_PROFILING=1 \
CORECLR_PROFILER={36032161-FFC0-4B61-B559-F6C5D41BAE5A} \
CORECLR_NEWRELIC_HOME=/usr/local/newrelic-dotnet-agent \
CORECLR_PROFILER_PATH=/usr/local/newrelic-dotnet-agent/libNewRelicProfiler.so \
NEW_RELIC_LICENSE_KEY=da5c4a50130315ddeb39ac1c2e65fe19FFFFNRAL \
NEW_RELIC_APP_NAME="dotnetCorrelationId"


# Comando para executar o aplicativo quando o contêiner for iniciado
ENTRYPOINT ["dotnet", "dotnetCorrelationId.dll"]
