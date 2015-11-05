00.dat
```
-2, 0
-1, 0
0, 2
1, 3
2, -5
3, 0
4, 0
```

_gnuplot commands_
```
gnuplot> unset key
gnuplot> unset border
gnuplot> sinc(x) = sin(pi*x)/(pi*x)
gnuplot> set xrange [-3.0:5.0]
gnuplot> set output "output.png"
gnuplot> set terminal png size 176,120
gnuplot> plot 2.0 * sinc(x) w l lt 1 lw 12, 3.0*sinc(x-1) w l lt 1 lw 12, \
 -5 *sinc(x-2) w l lt 1 lw 12, \
 2.0 * sinc(x)  + 3.0*sinc(x-1) - 5*sinc(x-2) w l lt 3 lw 16, \
 "00.dat" with points lt -1 pt 7 ps 2
```

アプリケーションアイコン用画像
```
gnuplot> unset key
gnuplot> unset border
gnuplot> sinc(x) = sin(pi*x)/(pi*x)
gnuplot> set output "ppwlogo1024.png"
gnuplot> set terminal png size 1024,1024
gnuplot> unset xtics
gnuplot> unset ytics
gnuplot> set xrange [-4.0:6.0]
gnuplot> set yrange [-8.0:8.0]
gnuplot> plot 2.0 * sinc(x) w l lt 1 lw 12, 3.0*sinc(x-1) w l lt 1 lw 12, \
-5 *sinc(x-2) w l lt 1 lw 12, 2.0 * sinc(x)  + 3.0*sinc(x-1) - \
5*sinc(x-2) w l lt 3 lw 16, "00.dat" with points lt -1 pt 7 ps 6

```