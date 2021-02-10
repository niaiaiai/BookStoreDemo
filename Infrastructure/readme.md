# 数据库迁移命令
    PM> Add-Migration Init -Context XXXContext -Project Infrastructure -StartupProject Infrastructure -Args '--environment Development'
    PM> Update-Database -Context XXXContext -Project Infrastructure -StartupProject Infrastructure -Args '--environment Development'

## 参数
可接受参数:
    -Args '--environment Development'
    -Args '--environment Production'