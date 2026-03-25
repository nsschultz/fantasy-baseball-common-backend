FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0
ARG TARGETARCH
ENV PATH="$PATH:/root/.dotnet/tools"
WORKDIR /app
RUN mkdir -p /usr/share/man/man1 /usr/share/man/man2 && \
    apt-get update && apt-get install -y --no-install-recommends default-jre && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* && \
    curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.7/install.sh | bash && \
    export NVM_DIR="$HOME/.nvm" && \
    [ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh" && \
    nvm install --lts && \
    nvm install 20.12 && \
    dotnet tool install --global dotnet-sonarscanner --version 11.1.0 && \
    useradd -u 5000 service-user && mkdir /app && chown -R service-user:service-user /app
ENV DOTNET_ROLL_FORWARD=Major
COPY scan.sh /scripts/scan.sh
RUN chmod +x /scripts/scan.sh