# -*- coding: utf-8
# 这是一个猜数字的游戏
import random #导入模块，便于使用函数
print('你好，你叫什么名字？')
myName = input() #要求玩家输入姓名
number = random.randint(1,100) #调用了一个名为randint()的新函数，该函数是由random提供的，并且把返回值存储到了变量number中
print('好的,'+ myName + ',我们来玩一个猜数字的游戏吧。我想了一个数字，它是在1到100这个范围内。') #告诉玩家数字的范围
print('你猜一下我想的是哪个数字？')  # 提示玩家输入一个数字
for i in range(7): #允许玩家猜测的次数
    guess = input() #要求玩家输入一个数字
    guess = int(guess)
    while guess<1 or guess>100:
        print('请输入一个在1-100这个范围内的数字！')
        guess = input()
        guess = int(guess)
        if guess<1 or guess>100:
            continue
        if guess>0 and guess<101:
            break
    if guess < number:
        print('你猜的数字太小啦！')
    if guess > number:
        print('你猜的数字太大啦！')
    if guess == number:
        break
if guess == number:
    guess = str(guess)
    print('恭喜你！'+ myName + ',我在心里想的那个数字就是'+ guess +'!')
else:
    number = str(number) #str() 函数将对象转化为适于人阅读的形式
    print('很可惜!'+ myName +',我想的数字是'+ number + '，继续努力哦!')




