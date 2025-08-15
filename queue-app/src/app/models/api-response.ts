export interface ApiResponse<T> {
  is_success: boolean;
  status_code: number;
  message: string;
  data: T;
}