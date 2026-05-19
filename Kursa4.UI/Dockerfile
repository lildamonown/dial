# ���� 1: ������
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# �������� ����� ��������
COPY ["Kursa4.UI/Kursa4.UI.csproj", "Kursa4.UI/"]
COPY ["Kursa4.BLL/Kursa4.BLL.csproj", "Kursa4.BLL/"]
COPY ["Kursa4.DAL/Kursa4.DAL.csproj", "Kursa4.DAL/"]

# ��������������� �����������
RUN dotnet restore "Kursa4.UI/Kursa4.UI.csproj"

# �������� ���� �������� ���
COPY . .

# �������� ������
WORKDIR "/src/Kursa4.UI"
RUN dotnet build "Kursa4.UI.csproj" -c Release -o /app/build

# ���� 2: ����������
FROM build AS publish
RUN dotnet publish "Kursa4.UI.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ���� 3: ������
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 80
EXPOSE 443

# �������� �������������� ����������
COPY --from=publish /app/publish .

# ������������� ���������� ��������� ��� �����
ENV ASPNETCORE_URLS=http://+:80
ENV ASPNETCORE_ENVIRONMENT=Development

ENTRYPOINT ["dotnet", "Kursa4.UI.dll"]