# import signal
# import main
#
#
# class TimeLimit:
#     # Define the function to handle the timeout
#     def __init__(self, run_time_alg, alg_function, pkgs, container_dim, real_pkgs=None):
#         self.timeout_duration = run_time_alg
#         self.alg_function = alg_function
#         self.pkgs = pkgs
#         self.cont_dim = container_dim
#         self.real_pkgs = real_pkgs
#
#     def timeout_handler(self, signum, frame):
#         raise TimeoutError("Execution time exceeded.")
#
#     # # Set the desired timeout duration in seconds
#     # timeout_duration = 3  # 10 seconds
#
#     def set_signal_and_alarm(self):
#         # Set the signal handler
#         signal.signal(signal.SIGALRM, self.timeout_handler)
#
#         # Set the alarm
#         signal.alarm(self.timeout_duration)
#
#     def run_algorithm(self):
#         self.set_signal_and_alarm()
#         try:
#             if not self.real_pkgs:
#                 return self.alg_function(self.pkgs, self.cont_dim)
#             return self.alg_function(self.pkgs, self.cont_dim, self.real_pkgs)
#
#         except TimeoutError:
#             if not self.real_pkgs:
#                 return main.FINAL_CONSTRUCTION_PACKAGING
#             return main.FINAL_IMPROVEMENT_PACKAGING
#
#         finally:
#             signal.alarm(0)

# def activate_func():
#     for i in range(1000):
#         print("Hello")
#
#
# t = TimeLimit(run_time_alg=5, alg_function=activate_func)
# t.run_algorithm()

#
#
# import csv
# import json
#
# csv_file = 'dataset.csv'
# json_file = 'input.json'
#
# container_data = {}
# package_data = []
#
# with open(csv_file, 'r') as file:
#     reader = csv.reader(file)
#     rows = list(reader)
#
# # Extract container information
# container_height = rows[1][1]
# container_width = rows[1][2]
# container_length = rows[1][3]
#
# container_data["Height"] = container_height
# container_data["Width"] = container_width
# container_data["Length"] = container_length
# container_data["Cost"] = "700"
#
# # Extract package information
# for row in rows[4:]:
#     if row[0] != '':
#         package = {
#
#             "DeliveryId": "10",
#             "Width": row[2],
#             "Height": row[1],
#             "Length": row[3],
#             "Order": row[0]
#         }
#         package_data.append(package)
#
# # Create JSON structure
# json_data = {
#     "Id": "100",
#     "Container": container_data,
#     "Packages": package_data
# }
#
# # Write JSON to file
# with open(json_file, 'w') as file:
#     json.dump(json_data, file, indent=4)


#
# import os
# import threading
# delay_time = 1   # delay time in seconds
#
# def my_potentially_never_ending_call():
#     for i in range(1222222222222):
#         pass
#     return "ff"
# def watchdog():
#   print('Watchdog expired. Exiting...')
#
#
# alarm = threading.Timer(delay_time, watchdog)
# alarm.start()
# x = my_potentially_never_ending_call()
# alarm.cancel()
# print(x)
import math

gc = 0
length = 1000
l = [100, 200, 300, 400, 700, 650]
gc = math.gcd(length, l[0])
for i in range(1, len(l) - 2):
    gc = math.gcd(gc, l[i + 1])
print(type(gc))



