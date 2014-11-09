Readtable
=========
# 功能特点
1. 读取在Excel中导出的以制表符分隔的txt数据文件
1. 对已经读取的文件进行格式化存取：
    - 系统默认序列化格式：Table->Serialize/Deserialize
    - 二进制格式：Table->SaveBinary/LoadBinary
1. 根据表记录字段信息生成表记录代码：RecordGenerater->Generate

# 核心文件
1. Main.cs 程序入口，包含所有功能的使用示例以及表数据存取性能测试代码
1. Logger.cs 日志工具
1. text.txt 测试表
1. Data/IRecord.cs 基于泛型的表记录的顶层接口
1. Data/Table.cs 基于泛型表记录类型的数据表的实现类
1. Data/Records/TestRecord.cs 测试表记录的实现类

# 运行环境
Monodevelop v4.0.1

# 注意事项
1. 对于该程序的性能测试，还不是很稳定，具体表现为，在不改变代码和资源的情况下，分别运行几次，然后改程序在控制台的输出结果都不同
1. 使用RecordGenerater->Generate之后生成的表记录会保存到工程更目录下的Data/Records/下，不会直接添加到工程中去，可以在Monodevelop中自定义解决方案／项目的选项，自动添加
