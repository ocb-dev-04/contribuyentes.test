import { Pagination as MuiPagination, Box } from "@mui/material";
import React from "react";

interface Props {
  page: number;
  totalPages: number;
  onChange: (page: number) => void;
}

const Pagination: React.FC<Props> = ({ page, totalPages, onChange }) => {
  if (totalPages <= 1) return null;
  return (
    <Box display="flex" justifyContent="center" mt={2}>
      <MuiPagination
        count={totalPages}
        page={page}
        onChange={(e, value) => onChange(value)}
        color="primary"
      />
    </Box>
  );
};

export default Pagination;
