import { Inject, Injectable } from '@nestjs/common';
import { ClientProxy, ClientProxyFactory, Transport } from '@nestjs/microservices';
import { catchError, of } from 'rxjs';

@Injectable()
export class AppService {
constructor(@Inject('USER_SERVICE') private readonly client: ClientProxy) {}  
  getHello(): string {
    return 'Hello World!';
  }

  async sum(a: number, b: number): Promise<number> {
    return this.client.send<number>({ cmd: 'sum' }, { a, b }).toPromise();
  }

  async sendData(data: any): Promise<any> {
    const otherClient = ClientProxyFactory.create({
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'user-response',
        queueOptions: {
          durable: false,
        },
      },
    });
    try {
      const payload = data;
      return await otherClient.emit("other.created", payload).toPromise();
    }
    catch (err) {
      console.log(err);
    }
  }
}
