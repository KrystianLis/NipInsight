export type CompanyRequestData = {
    nip: string;
    date: string;
  };
  
export type CompanyResult = {
    nip: string | null | undefined;
    name: string | null | undefined;
    regon: string | null | undefined;
    residenceAddress: string | null | undefined;
  };