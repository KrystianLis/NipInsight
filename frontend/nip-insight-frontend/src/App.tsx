import { Navigate, Route, Routes } from "react-router-dom";
import React from "react";
import { CircularProgress } from "@mui/material";

const PageNotFound = React.lazy(() => import("./Components/PageNotFound"));
const CompanyForm = React.lazy(
  () => import("./Components/CompanyForm/CompanyForm")
);

function App() {
  return (
    <div className="App">
      <Routes>
        <Route
          path="/"
          element={
            <React.Suspense
              fallback={
                <>
                  <CircularProgress />
                </>
              }
            >
              <CompanyForm />
            </React.Suspense>
          }
        />
        <Route
          path="/404"
          element={
            <React.Suspense
              fallback={
                <>
                  <CircularProgress />
                </>
              }
            >
              <PageNotFound />
            </React.Suspense>
          }
        />
        <Route path="*" element={<Navigate to="/404" />} />
      </Routes>
    </div>
  );
}

export default App;
