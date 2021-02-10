# BookStoreDemo
书店系统demo 是针对书店后台服务人员操作的demo，使用了 MyFramework 框架，ddd 架构，前后端分离。
* demo: https://studydemo.online:8081/

# 启动项目
git clone https://github.com/niaiaiai/BookStoreDemo.git

设置多启动项目，将 Ids4 和 Web 项目启动

# 用命令行创建项目模板
    dotnet new -i MyFramework.Templates --nuget-source http://studydemo.online:13564/v3/index.json
    dotnet new Myfw -n [项目名称]

# 依赖版本
* .net 5.0
* NLog 4.7
* NLog.Web.AspNetCore 4.10
* Swashbuckle.AspNetCore 5.6
* Ardalis.GuardClauses 3.0
* EF Core 5.0
* Volo.Abp.Specifications 4.0
* IdentityServer4 4.1
* MyEntity 1.1
* MyEntityFrameworkCore 1.0
* MyRepositories 1.1
* MyServices 1.0
* Utils 1.1
