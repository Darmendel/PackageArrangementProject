import matplotlib.pyplot as plt
import mpl_toolkits.mplot3d.axes3d as p3
import random

colors = [(random.uniform(0, 1), random.uniform(0, 1), random.uniform(0, 1)) for i in range(100)]

with open("outputmdclp.txt", 'r') as f:
    data = f.readlines()

rx, ry, rz = 1600, 800, 600
fig = plt.figure()
ax = fig.add_subplot(projection='3d')

ax.bar3d(0, 0, 0, rx, ry, rz, color=(0, 0, 0, 0), shade=False)

type_0 = plt.Rectangle((0, 0), 1, 1, fc=colors[0])
type_1 = plt.Rectangle((0, 0), 1, 1, fc=colors[1])
type_2 = plt.Rectangle((0, 0), 1, 1, fc=colors[2])
type_3 = plt.Rectangle((0, 0), 1, 1, fc=colors[3])
type_4 = plt.Rectangle((0, 0), 1, 1, fc=colors[4])
total_amount = 0
for val, line in enumerate(data):
    # Creating switch with appropiate colors'
    # if val == 7 or val == 8:

    dx, dy, dz, x, y, z = [float(x) for x in line.split()]
    # print(x, y, z)
    # print(x + dx, y + dy, z + dz)
    # if val == 5:
    #     z += 300
    ax.bar3d(x, y, z, dx, dy, dz, color=colors[val], edgecolor=(0, 0, 0, 1), shade=False)
    total_amount += dx * dy * dz
print(total_amount)
ax.set_title('Visualization of placed switches')
#
plt.show()
index = 0
for val, line in enumerate(data):
    dx, dy, dz, x, y, z = [float(x) for x in line.split()]
    e1_x, e1_y, e1_z = x + dx, y + dy, z + dz
    index = val + 1
    for sec_line in data[val + 1:]:
        d2x, d2y, d2z, x2, y2, z2 = [float(x) for x in sec_line.split()]
        e2_x, e2_y, e2_z = x2 + d2x, y2 + d2y, z2 + d2z
        if e1_x > x2 + 0.001 and e1_y > y2 + 0.001 and e1_z > z2 + 0.001 \
                and z + 0.001 < e2_z and y + 0.001 < e2_y and x + 0.001 < e2_x:

            print(f"e1_x: {x}, x: {e1_x} ")
            print(f"e2_x: {y}, y: {e1_y} ")
            print(f"e1_x: {z}, z: {e1_z} ")
            print(f"e2_x: {x2}, x2: {e2_x} ")
            print(f"e2_x: {y2}, y2: {e2_y} ")
            print(f"e2_x: {z2}, z2: {e2_z} ")
            print(val, index)

# input[1,2,3,4,5]
# [1] - 1
# [2], [1,2,3] -2
# [3], [2,3,4]- 3
# [4], [3,4,5] -1
# [5] - 1
# output[1,2,2,1,1]

