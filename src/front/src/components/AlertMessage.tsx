import { Alert, Box } from "@mui/material";
import React from "react";

interface Props {
  severity?: "error" | "warning" | "info" | "success";
  message?: string;
}

const AlertMessage: React.FC<Props> = ({ severity = "info", message }) => {
  if (!message) return null;
  return (
    <Box my={2}>
      <Alert severity={severity}>{message}</Alert>
    </Box>
  );
};

export default AlertMessage;
