import pika
import json
import uuid
# program.cs / data.cs
connection = pika.BlockingConnection(pika.ConnectionParameters('localhost'))
channel = connection.channel()

channel.exchange_declare(
    exchange='order',
    exchange_type='direct'
)

order = {
    'id': str(uuid.uuid4()),
    'user_email': 'samkatz@gmail.com',
    'pkg': 'id length, width, height',
    'quantity': 10
}

channel.basic_publish(
    exchange='order',
    routing_key='order.notify',
    body=json.dumps({'pacakge_data': order['user_email']})
)

print('smaller success')

channel.basic_publish(
    exchange='order',
    routing_key='order.report',
    body=json.dumps(order)
)

print('bigger success')

connection.close()

