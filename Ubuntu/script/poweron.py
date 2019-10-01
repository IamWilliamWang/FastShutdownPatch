from datetime import datetime

dt = datetime.now()
power_on_text = 'INSERT INTO [Table](开机时间) VALUES (GETDATE())'.replace('GETDATE()',
                                                                       "'" + dt.strftime('%Y-%m-%d %H:%M:%S.%f') + "'")
power_on_text += '鋝'
with open('TimeDatabase.cache', 'a') as f:
    f.write(power_on_text)
print('插入开机时间成功！')
