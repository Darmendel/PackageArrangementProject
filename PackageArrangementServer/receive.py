import pika
import json

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
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_consume(on_message_callback=callback, queue=queue_name)
print("waiting for messages")
channel.start_consuming()
