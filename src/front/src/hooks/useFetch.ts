import { useState, useEffect } from "react";
import type { PaginatedCollection } from "../types/PaginatedCollection";

export const useFetchPaginated = <T,>(
  fetchFn: (page: number) => Promise<PaginatedCollection<T>>
) => {
  const [data, setData] = useState<T[]>([]);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [loading, setLoading] = useState(false);
  const [isLastPage, setIsLastPage] = useState(false);

  useEffect(() => {
    setLoading(true);
    fetchFn(page).then((res) => {
      setData(res.data);
      setTotalPages(res.totalPages);
      setIsLastPage(res.isLastPage);
      setLoading(false);
    });
  }, [page]);

  return { data, page, setPage, totalPages, loading, isLastPage };
};
