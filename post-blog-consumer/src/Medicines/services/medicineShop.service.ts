// MedicineShopService.ts
import { Injectable } from '@nestjs/common';
import { InjectRepository } from '@nestjs/typeorm';
import { Repository } from 'typeorm';
import { MedicineShopPostEntity } from '../models/post.entity';
import { MedicineShopPost } from '../models/post.interface';
import { ClientProxy, ClientProxyFactory, Transport } from '@nestjs/microservices';

@Injectable()
export class MedicineShopService {
  private readonly otherClient: ClientProxy;

  constructor(
    @InjectRepository(MedicineShopPostEntity)
    private readonly medicineShopPostRepository: Repository<MedicineShopPostEntity>,
  ) {
    this.otherClient = ClientProxyFactory.create({
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'medicine-response',
        queueOptions: {
          durable: false,
        },
      },
    });
  }

  async findAll(): Promise<MedicineShopPost[]> {
    return await this.medicineShopPostRepository.find();
  }

  async findOne(id: number): Promise<MedicineShopPost> {
    const query = this.medicineShopPostRepository.createQueryBuilder('medicineShopPost');
    query.where('medicineShopPost.id = :id', { id: id });
    return await query.getOne();
  }

  async create(medicineShopPost: MedicineShopPost): Promise<MedicineShopPost> {
    return await this.medicineShopPostRepository.save(medicineShopPost);
  }

  async update(id: number, medicineShopPost: MedicineShopPost): Promise<MedicineShopPost> {
    await this.medicineShopPostRepository.update(id, medicineShopPost);
    return await this.findOne(id);
  }

  async delete(id: number): Promise<any> {
    return await this.medicineShopPostRepository.delete(id);
  }

  async sendData(data: any): Promise<any> {
    try {
      const payload = data;
      return await this.otherClient.emit("other.created", payload).toPromise();
    } catch (err) {
      console.log(err);
    }
  }
}
