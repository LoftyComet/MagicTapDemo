'''
Author: LoftyComet 1277173875@qq。
Date: 2024-07-16 12:43:19
LastEditors: LoftyComet 1277173875@qq。
LastEditTime: 2024-07-16 12:43:33
FilePath: \Py\test.py
Description: 

Copyright (c) 2024 by ${git_name_email}, All Rights Reserved. 
'''
from mayavi import mlab
import numpy as np

# 生成示例数据
x, y, z = np.mgrid[-5:5:64j, -5:5:64j, -5:5:64j]
data = np.sin(np.sqrt(x**2 + y**2 + z**2))

# 创建体积数据
vol = mlab.pipeline.scalar_field(x, y, z, data)

# 设置颜色映射和透明度
vol.volume.cmap = 'hot'  # 选择颜色映射
vol.volume.opacity = 0.5  # 设置透明度

# 显示坐标轴和标题
mlab.axes()
mlab.title('3D Heatmap')

# 显示图形
mlab.show()