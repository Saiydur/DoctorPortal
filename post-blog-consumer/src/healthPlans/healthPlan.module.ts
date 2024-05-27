import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { HealthPlanPostEntity } from './models/post.entity';
import { HealthPlanService } from './services/healthPlan.service';
import { HealthPlanController } from './controllers/healthPlan.controller';
import { ClientsModule, Transport } from '@nestjs/microservices';

@Module({
  imports: [
    TypeOrmModule.forFeature([HealthPlanPostEntity]),
    ClientsModule.register([
      {
        name: 'HEALTH_SERVICE',
        transport: Transport.RMQ,
        options: {
          urls: ['amqp://localhost:5672'],
          queue: 'health',
          queueOptions: {
            durable: false,
          },
        },
      },
    ]),
  ],
  providers: [HealthPlanService],
  controllers: [HealthPlanController],
})
export class HealthPlanModule {}
