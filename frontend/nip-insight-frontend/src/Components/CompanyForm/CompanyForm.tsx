import React from "react";
import { useForm } from "react-hook-form";
import {
  Container,
  TextField,
  Button,
  Grid,
  CircularProgress,
} from "@mui/material";
import axios from "axios";
import { yupResolver } from "@hookform/resolvers/yup";
import * as yup from "yup";
import "../../App.css";
import { CompanyRequestData, CompanyResult } from "./CompanyForm.types";
import { API_URL } from "../../config/config";

const schema = yup.object().shape({
  nip: yup
    .string()
    .required("NIP is required")
    .matches(/^\d{10}$/, "NIP must be 10 digits"),
  date: yup.string().required("Date is required"),
});

function CompanyForm() {
  const {
    handleSubmit,
    register,
    formState: { errors },
  } = useForm<CompanyRequestData>({
    resolver: yupResolver(schema),
  });

  const [companyInfo, setCompanyInfo] = React.useState<CompanyResult | null>(
    null
  );
  const [isError, setIsError] = React.useState(false);
  const [isRequestPending, setIsRequestPending] = React.useState(false);
  const onSubmit = async ({ nip, date }: CompanyRequestData) => {
    setIsError(false);
    if (!isRequestPending) {
      setIsRequestPending(true);
      const url = `${API_URL}${nip}`;
      const formattedDate = new Date(date).toLocaleDateString("en-CA");
      await axios
        .get(url, {
          params: {
            date: formattedDate,
            headers: {
              "Content-Type": "application/json",
              Accept: "application/json",
            },
          },
        })
        .then((response) => {
          setCompanyInfo(response.data);
        })
        .catch((err) => {
          setIsError(true);
          console.log(err, "error");
        })
        .finally(() => {
          setIsRequestPending(false);
        });
    }
  };

  return (
    <Container maxWidth="sm">
      <h1 className="heading">Company Information</h1>
      <form onSubmit={handleSubmit(onSubmit)}>
        <Grid container direction="column">
          <TextField
            label="NIP"
            {...register("nip")}
            error={!!errors.nip}
            helperText={errors.nip?.message}
          />
          <TextField
            type="date"
            {...register("date")}
            error={!!errors.date}
            helperText={errors.date?.message}
          />
          <Button type="submit" variant="contained" color="primary">
            Get Data
          </Button>
        </Grid>
      </form>
      {isRequestPending ? (
        <div>
          <CircularProgress />
        </div>
      ) : isError ? (
        <div>Error occurred</div>
      ) : companyInfo ? (
        <div>
          <h2>Company Details</h2>
          <p>NIP: {companyInfo?.nip}</p>
          <p>Name: {companyInfo?.name}</p>
          <p>REGON: {companyInfo?.regon}</p>
          <p>Residence Address: {companyInfo?.residenceAddress}</p>
        </div>
      ) : null}
    </Container>
  );
}

export default CompanyForm;
