clc;
close all;
clear;

theta = linspace(0, 360, 36);

XX = 10 * cos(theta * pi / 180);
YY = 10 * sin(theta * pi / 180);
ZZ = linspace(0, 10, 36);
X =[]; Y =[]; Z =[];

hold on;
grid on;
for i = 1:1
    Z(i,:) = i * ones(length(ZZ), 1);
    Y(i,:) = i * YY';
    X(i,:) = XX'/i;
    plot3(X(i,:), Y(i,:), Z(i,:));
end;

hold off;

view(45, 30);
