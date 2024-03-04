export type Pet = {
  id: number;
  name: string;
  breed: string;
  dateOfBirth: string;
  color: string;
  gender: string;
  weight: number;
  isNeutered: boolean;
  ownerId: string;
  petPhoto: PetPhoto[];
};

export type PetPhoto = {
  id: number;
  url: string;
  isMain: boolean;
  petId: number;
};
