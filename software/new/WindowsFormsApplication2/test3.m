clc;
close all;
clear;

theta = linspace(0, 360, 37);

C = linspace(0, 1, 4);

sensor_data_1 = ones(1, 37) * 1 * 10;
sensor_data_1(37) = sensor_data_1(1);   
X_1 = sensor_data_1 .* cos(theta * pi / 180);
Y_1 = sensor_data_1 .* sin(theta * pi / 180);
Z_1 = 1 * ones(1, 37);

sensor_data_2 = ones(1, 37) * 2 * 10;
sensor_data_2(37) = sensor_data_2(1);   
X_2 = sensor_data_2 .* cos(theta * pi / 180);
Y_2 = sensor_data_2 .* sin(theta * pi / 180);
Z_2 = 2 * ones(1, 37);

sensor_data_3 = ones(1, 37) * 2.5 * 10;
sensor_data_3(37) = sensor_data_3(1);   
X_3 = sensor_data_3 .* cos(theta * pi / 180);
Y_3 = sensor_data_3 .* sin(theta * pi / 180);
Z_3 = 3 * ones(1, 37);

%%
plot3(X_1, Y_1, Z_1);
hold on;
plot3(X_2, Y_2, Z_2);
plot3(X_3, Y_3, Z_3);


%%
for ii = 1 : 36
    X = [X_1(ii), X_1(ii + 1), X_2(ii+1), X_2(ii)];
    Y = [Y_1(ii), Y_1(ii + 1), Y_2(ii+1), Y_2(ii)];
    Z = [Z_1(ii), Z_1(ii + 1), Z_2(ii+1), Z_2(ii)];
    
    fill3(X, Y, Z, C, 'EdgeColor', 'none')
    alpha(0.5);
    hold on;
end

for ii = 1 : 36
    X = [X_2(ii), X_2(ii + 1), X_3(ii+1), X_3(ii)];
    Y = [Y_2(ii), Y_2(ii + 1), Y_3(ii+1), Y_3(ii)];
    Z = [Z_2(ii), Z_2(ii + 1), Z_3(ii+1), Z_3(ii)];
    
    fill3(X, Y, Z, C, 'EdgeColor', 'none')
    alpha(0.5);
    
    hold on;
end

    
xlabel('X'), ylabel('Y'), zlabel('Z');
grid on;
view(45, 45);