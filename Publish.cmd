@echo off
@echo -----------------------------------------------------------------------------
@echo *** ��ӭʹ�� ZKEACMS ***
@echo -----------------------------------------------------------------------------
@echo �ù��߽��������������� ZKEACMS ����
@echo �ڿ�ʼ֮ǰ�����Ȳ鿴���ǵ����Э��
@echo http://www.zkea.net/licenses
@echo -----------------------------------------------------------------------------
@echo �й���.Net CoreӦ�ó���İ�װ������鿴 Microsoft Docs
@echo .NET Core Ӧ�ó�����
@echo https://docs.microsoft.com/zh-cn/dotnet/articles/core/deploying
@echo .NET Core ����ʱ��ʶ�� (RID) Ŀ¼
@echo https://docs.microsoft.com/zh-cn/dotnet/articles/core/rid-catalog
@echo -----------------------------------------------------------------------------
@pause
cd src/ZKEACMS.WebHost
dotnet publish -c Release -o ./bin/Release/PublishOutput
cd ../../
cd PluginPublisher
dotnet restore
dotnet run
cd ../
set current_path=%cd%
explorer %current_path%\src\ZKEACMS.WebHost\bin\Release\PublishOutput