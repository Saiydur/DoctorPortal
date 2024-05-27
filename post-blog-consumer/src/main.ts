import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';
import { MicroserviceOptions, Transport } from '@nestjs/microservices';

async function bootstrap() {
  const microserviceOptions: MicroserviceOptions = {
    transport: Transport.RMQ,
    options: {
      urls: ['amqp://localhost:5672'],
      queue: 'microservice_queue',
      queueOptions: {
        durable: false
      },
      prefetchCount: 1,
      isGlobalPrefetchCount: true,
    },
  };
  const app = await NestFactory.create(AppModule);
  const microservice = app.connectMicroservice(microserviceOptions);
  await app.startAllMicroservices();
}
((): void => {
  bootstrap()
      .then(() => process.stdout.write(`Listening on port \n`))
      .catch((err) => {
          process.stderr.write(`Error: ${err.message}\n`);
          process.exit(1);
      });
})();
