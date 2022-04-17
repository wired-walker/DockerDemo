#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS dotnet_build
WORKDIR /src
COPY ["WebApp/WebApp.csproj", "WebApp/"]
RUN dotnet restore "WebApp/WebApp.csproj"
COPY . .
WORKDIR "/src/WebApp"
RUN dotnet build "WebApp.csproj" -c Release -o /app/build


FROM node:12.18.1 as npm_install
WORKDIR C:\\app
RUN npm config set registry https://registry.npmjs.org/

COPY ./WebApp/client-app/package.json .
RUN dir

RUN npm install 


FROM node:12.18.1 as react_build
WORKDIR C:\\app
COPY --from=npm_install C:\\app .
COPY ./WebApp/client-app .
RUN cat package.json
RUN ls -al ./node_modules/react-scripts/bin
RUN npm run build
RUN pwd


FROM dotnet_build AS publish
RUN dotnet publish "WebApp.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=react_build C:\\app ./wwwroot
ENTRYPOINT ["dotnet", "WebApp.dll"]

