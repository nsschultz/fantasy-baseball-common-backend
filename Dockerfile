FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0
ARG TARGETARCH
RUN mkdir -p /usr/share/man/man1 /usr/share/man/man2 /usr/local/dotnet-tools /scripts && \
    apt-get update && apt-get install -y --no-install-recommends default-jre && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/* && \
    useradd --create-home --shell /bin/bash appuser && \
    chown -R appuser:appuser /scripts /usr/local/dotnet-tools
COPY scan.sh /scripts/scan.sh
RUN chmod +x /scripts/scan.sh
USER appuser
RUN curl -o- https://raw.githubusercontent.com/nvm-sh/nvm/v0.39.7/install.sh | bash && \
    export NVM_DIR="$HOME/.nvm" && \
    [ -s "$NVM_DIR/nvm.sh" ] && \. "$NVM_DIR/nvm.sh" && \
    nvm install --lts && \
    nvm install 20.12 && \
    dotnet tool install --tool-path /usr/local/dotnet-tools dotnet-sonarscanner --version 11.1.0
ENV PATH="$PATH:/usr/local/dotnet-tools:/home/appuser/.dotnet/tools"
ENV DOTNET_ROLL_FORWARD=Major