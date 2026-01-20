export interface CustomerPreview {
  customerId: string;
  fullName: string;
  dateOfBirth?: string;
  age: number;
  homeAddress: Address;
}

export interface Address {
  street?: string;
  city?: string;
  state?: string;
  zipCode?: string;
  country?: string;
}
