#-*- coding: utf-8
import turtle
import time
a = input("您好呀！请输入您的名字：")
print("{},请您睁大眼睛，静待5秒钟！".format(a))
time.sleep(5)
print('''          *****      *         ***   ***    *             *   *********       *     *     ***     *       *    **
.           *        *       **   * *   **   *           *    *                *   *     *   *    *       *    **
.           *        *        *    *    *     *         *     *                 * *     *     *   *       *    **
.           *        *         *       *       *       *      *********          *     *       *  * {}*    **    
.           *        *          *     *         *     *       *                  *     *       *  *       *    **
.           *        *           *   *           *   *        *                  *      *     *   *       *    **
.           *        *            * *             * *         *                  *       *   *     *     *     
.         *****      *******       *               *          *********          *        ***       *****      **'''.format(a))
from turtle import *
def love():
    for i in range(200):
        right(1)
        forward(1)
color('red','pink')
pensize(2)
begin_fill()

left(140)
forward(111.65)
love()
left(120)
love()
forward(111.65)
end_fill()
up()
goto(100,-10)
write("I Love you")
hideturtle()
done()
0
