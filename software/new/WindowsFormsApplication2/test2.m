clc;
close all;
clear;

theta = linspace(0, 360, 37);

C = ones(1, 37);

 
for i = 1 : 2
    sensor_data = ones(1, 37) * i * 10;
    sensor_data(37) = sensor_data(1);
    
    X = sensor_data .* cos(theta * pi / 180);
    Y = sensor_data .* sin(theta * pi / 180);
    Z = i * ones(1, 37);
    %plot3(X, Y, Z, 'LineWidth', 5);
    fill3(X, Y, Z, C)
    alpha(0.5);
    hold on;
end
xlabel('X'), ylabel('Y'), zlabel('Z');
hold off;
grid on;
view(45, 45);