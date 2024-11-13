'''
Author: LoftyComet 1277173875@qq.com
Date: 2023-07-31 16:53:05
LastEditors: LoftyComet 1277173875@qq.com
LastEditTime: 2023-08-05 13:40:29
Description: 

Copyright (c) 2023 by ${git_name_email}, All Rights Reserved. 
'''
import numpy as np
import sys
r = 7.
if sys.argv[2]:
    r = float(sys.argv[2])
d = r / np.sqrt(2)
k = 30
width = 100
height = 100
nx = int(width / d) + 1
ny = int(height / d) + 1

occupied = np.zeros((ny, nx))
occupied_coord = np.zeros((ny, nx, 2))
active_list = []
sampled = []

relative = np.array([[-1, 2], [0, 2], [1, 2],
                     [-2, 1], [-1, 1], [0, 1], [1, 1], [2, 1],
                     [-2, 0], [-1, 0], [1, 0], [2, 0],
                     [-2, -1], [-1, -1], [0, -1], [1, -1], [2, -1],
                     [-1, -2], [0, -2], [1, -2]])
if sys.argv[1]:
    seed = int(sys.argv[1])
else:
    seed = 0
np.random.seed(seed)
x, y = np.random.rand() * width, np.random.rand() * height
idx_x, idx_y = int(x / d), int(y / d)
occupied[idx_y, idx_x] = 1
occupied_coord[idx_y, idx_x] = (x, y)
active_list.append((x, y))
sampled.append((x, y))

# x, y = 1, 1
# x = [0.6, 0.577, 0.512, 0.415, 0.3, 0.19, 0.088, 0.023, 0, 0.023, 0.088, 0.185, 0.3, 0.415, 0.512 ,0.577]
# y = [0.3, 0.415, 0.512 ,0.577, 0.6, 0.577, 0.512, 0.415, 0.3, 0.19, 0.088, 0.023, 0, 0.023, 0.088, 0.185]
# x = [0.6, 0.577]
# y = [0.3, 0.415]
# for i in range(len(x)):
#     tempX = x[i] * 100 + 20
#     tempY = y[i] * 100 + 20
#     idx_x, idx_y = int(tempX / d), int(tempY / d)
#     occupied[idx_y, idx_x] = 1
#     occupied_coord[idx_y, idx_x] = (tempX, tempY)
#     active_list.append((tempX, tempY))
#     sampled.append((tempX, tempY))

sampled_idx = 0
while len(active_list) > 0:

    idx = np.random.choice(np.arange(len(active_list)))
    ref_x, ref_y = active_list[idx]
    radius = (np.random.rand(k) + 1) * r
    theta = np.random.rand(k) * np.pi * 2
    candidate = radius * np.cos(theta) + ref_x, radius * np.sin(theta) + ref_y
    flag_out = False
    for _x, _y in zip(*candidate):
        if _x < 0 or _x > width or _y < 0 or _y > height:
            continue
        # other geo constraints
        flag = True
        idx_x, idx_y = int(_x / d), int(_y / d)
        if occupied[idx_y, idx_x] != 0:
            continue
        else:
            neighbours = relative + np.array([idx_x, idx_y])
        for cand_x, cand_y in neighbours:
            if cand_x < 0 or cand_x >= nx or cand_y < 0 or cand_y >= ny:
                continue
            if occupied[cand_y, cand_x] == 1:
                cood = occupied_coord[cand_y, cand_x]
                if (_x - cood[0]) ** 2 + (_y - cood[1]) ** 2 < r ** 2:
                    flag = False
                    break
        if flag:
            flag_out = True
            occupied[idx_y, idx_x] = 1
            occupied_coord[idx_y, idx_x] = (_x, _y)
            sampled.append((_x, _y))
            active_list.append((_x, _y))
            sampled_idx += 1
            break
    if not flag_out:
        active_list.pop(idx)

for temp in sampled:
    print(temp[0] / 100)
    print(temp[1] / 100)


