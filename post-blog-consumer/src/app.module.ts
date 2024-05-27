import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { AppService } from './app.service';
import { ConfigModule } from '@nestjs/config';
import { TypeOrmModule } from '@nestjs/typeorm';
import { ClientsModule, Transport } from '@nestjs/microservices';
import { MedicineShopModule } from './Medicines/medicineShop.module';
import { HealthPlanModule } from './healthPlans/healthPlan.module';
import { HospitalModule } from './hospitals/hospital.module';

@Module({
  imports: [
    ClientsModule.register([
      {
        name: 'USER_SERVICE',
        transport: Transport.RMQ,
        options: {
          urls: ['amqp://localhost:5672'],
          queue: 'user',
          queueOptions: {
            durable: false
          },
        },
      },
    ]),
    TypeOrmModule.forRoot({
      type: 'postgres',
      host: "localhost",
      port: 5432,
      username: "postgres",
      password: "root",
      database: "Health-Care",
      autoLoadEntities: true,
      synchronize: true,
    }),
    HospitalModule,
    HealthPlanModule,
    MedicineShopModule,
    ConfigModule.forRoot({ isGlobal: true }),
  ],
  controllers: [AppController],
  providers: [AppService,],
})
export class AppModule {
}
