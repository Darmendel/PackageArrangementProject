import pika
import json
import main

connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()

queue = channel.queue_declare('order_report')
queue_name = queue.method.queue


# channel.queue_bind(
#     exchange='order',
#     queue=queue_name,
#     routing_key='order_report'
# )


def callback(ch, method, properties, body):
    payload = json.loads(body)
    print(f'notifying {payload}')
    print("done")
    main.start(boxes_json=payload)
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_consume(on_message_callback=callback, queue=queue_name)
print("waiting for messages")
channel.start_consuming()






############ for testing
# def temp(payload):
#     main.start(boxes_json=payload)
#


# if __name__ == "__main__":
#     d = {
#     "Id": "100",
#     "Container": {
#         "Height": "600",
#         "Width": "800",
#         "Length": "1600",
#         "Cost": "700"
#     },
#     "Packages": [
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "1"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "2"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "300",
#             "Order": "3"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "300",
#             "Order": "4"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "5"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "600",
#             "Order": "6"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "600",
#             "Order": "7"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "600",
#             "Order": "8"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "600",
#             "Order": "9"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "600",
#             "Order": "10"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "600",
#             "Order": "11"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "600",
#             "Order": "12"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "200",
#             "Order": "13"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "300",
#             "Order": "14"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "200",
#             "Order": "15"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "300",
#             "Order": "16"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "800",
#             "Order": "17"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "200",
#             "Order": "18"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "300",
#             "Order": "19"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "300",
#             "Order": "20"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "21"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "22"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "23"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "200",
#             "Order": "24"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "200",
#             "Order": "25"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "26"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "27"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "28"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "29"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "30"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "800",
#             "Order": "31"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "1200",
#             "Order": "32"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "100",
#             "Height": "200",
#             "Length": "600",
#             "Order": "33"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "34"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "35"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "36"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "600",
#             "Order": "37"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "38"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "39"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "40"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "400",
#             "Order": "41"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "800",
#             "Order": "42"
#         },
#         {
#             "DeliveryId": "10",
#             "Width": "200",
#             "Height": "200",
#             "Length": "600",
#             "Order": "43"
#         }
#     ],
#      "UserId": "-NWSG59p56SkI3cELux0"
# }
#
#     temp(payload=d)


