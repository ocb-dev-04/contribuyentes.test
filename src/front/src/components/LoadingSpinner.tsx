import { CircularProgress, Box } from "@mui/material";
import React from "react";

const LoadingSpinner: React.FC = () => (
  <Box display="flex" justifyContent="center" alignItems="center" p={3}>
    <CircularProgress />
  </Box>
);

export default LoadingSpinner;
