import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { HospitalService } from './services/hospital.service';
import { HospitalPostEntity } from './models/post.entity';
import { HospitalController } from './controllers/hospital.controller';
import { ClientsModule, Transport } from '@nestjs/microservices';

@Module({
  imports: [TypeOrmModule.forFeature([HospitalPostEntity]),
  ClientsModule.register([
    {
      name: 'HOSPITAL_SERVICE',
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'hospital',
        queueOptions: {
          durable: false
        },
      },
    },
  ]),
  ],
  providers: [HospitalService],
  controllers: [HospitalController],
})
export class HospitalModule {}
