import { Module } from '@nestjs/common';
import { TypeOrmModule } from '@nestjs/typeorm';
import { MedicineShopController } from './controllers/medicineShop.controller';
import { MedicineShopService } from './services/medicineShop.service';
import { MedicineShopPostEntity } from './models/post.entity';
import { ClientsModule, Transport } from '@nestjs/microservices';

@Module({
  imports: [
    TypeOrmModule.forFeature([MedicineShopPostEntity]),
    ClientsModule.register([
      {
        name: 'MEDICINE_SERVICE',
        transport: Transport.RMQ,
        options: {
          urls: ['amqp://localhost:5672'],
          queue: 'medicine',
          queueOptions: {
            durable: false,
          },
        },
      },
    ]),
  ],
  providers: [MedicineShopService],
  controllers: [MedicineShopController],
})
export class MedicineShopModule {}
