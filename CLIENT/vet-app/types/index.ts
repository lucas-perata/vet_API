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

export type Vaccine = {
  id: number;
  name: string;
  required: boolean;
  sideEffects: string;
};

export type MedicalHistory = {
  id: number;
  date: string;
  description: string;
  treatment: string;
  vetName: string;
};

export type Expense = {
  id: number;
  category: string;
  description: string;
  amount: number;
  date: Date | string;
};

export type ExpensesPerCat = {
  categoryName: string;
  total: number;
};
