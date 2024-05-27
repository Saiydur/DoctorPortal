import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';

@Entity('hospital_post')
export class HospitalPostEntity {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({
    default: '',
  })
  name: string;

  @Column({
    default: '',
  })
  address: string;

  @Column({
    type: 'timestamp',
    default: () => 'CURRENT_TIMESTAMP',
  })
  createdAt: Date;
}
