'''
Author: LoftyComet 1277173875@qq.com
Date: 2023-07-31 18:24:35
LastEditors: LoftyComet 1277173875@qq。
LastEditTime: 2024-07-15 20:37:18
Description: 

Copyright (c) 2023 by ${git_name_email}, All Rights Reserved. 
'''
import numpy as np
import matplotlib.pyplot as plt
from scipy.spatial import KDTree
import sys
def poisson_disk_sampling(width, height, depth, r):
    # Step 0: Initialize an empty "point list" and "processing list"
    point_list = []
    processing_list = []

    # Step 1: Select the initial sample
    initial_sample = np.array([np.random.uniform(0, width), np.random.uniform(0, height), np.random.uniform(0, depth)])
    point_list.append(initial_sample)
    processing_list.append(initial_sample)

    # Step 2: While the processing list is not empty, execute the algorithm
    while processing_list:
        point = processing_list.pop(np.random.randint(len(processing_list)))
        for _ in range(30):  # This is the recommended retries count
            # Generate a point uniformly at random from the "annulus" around the point
            rho = np.sqrt(3 * r**2 * (np.random.uniform() + 1))
            theta = np.random.uniform(0, 2 * np.pi)
            phi = np.random.uniform(0, np.pi)
            new_point = point + rho * np.array([np.sin(phi) * np.cos(theta), np.sin(phi) * np.sin(theta), np.cos(phi)])

            # Check if the point is within the bounds and far enough from existing points
            if 0 <= new_point[0] < width and 0 <= new_point[1] < height and 0 <= new_point[2] < depth:
                if not point_list:
                    point_list.append(new_point)
                    processing_list.append(new_point)
                else:
                    tree = KDTree(point_list)
                    d, _ = tree.query(new_point)
                    if d >= r:
                        point_list.append(new_point)
                        processing_list.append(new_point)

    return np.array(point_list)

# r = 7.
# if sys.argv[2]:
#     r = float(sys.argv[2])

# Generate the point cloud
# point_cloud = poisson_disk_sampling(1, 1, 0.3, 0.08).tolist()
point_cloud = poisson_disk_sampling(1, 1, 0.3, 0.08).tolist()
for temp in point_cloud:
    temp[0] -= 0.5
    temp[1] -= 0.5

    # if temp[0]*temp[0] + temp[1]*temp[1] >= 0.16:
    #     continue
    print(temp[0])
    print(temp[1])
    print(temp[2])

# Plot the point cloud
# fig = plt.figure()
# ax = fig.add_subplot(111, projection='3d')
# ax.scatter(point_cloud[:, 0], point_cloud[:, 1], point_cloud[:, 2])
# plt.show()
