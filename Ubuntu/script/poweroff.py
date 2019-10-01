from datetime import datetime

dt = datetime.now()
power_off_text = 'UPDATE [Table] SET 关机时间 = GETDATE(), 时长 = GETDATE() - 开机时间 WHERE 序号 in (SELECT MAX(序号) FROM[Table])'.replace(
    'GETDATE()', "'" + dt.strftime('%Y-%m-%d %H:%M:%S.%f') + "'")
power_off_text += '鋝'
with open('TimeDatabase.cache', 'a') as f:
    f.write(power_off_text)
print('插入关机时间成功！')
