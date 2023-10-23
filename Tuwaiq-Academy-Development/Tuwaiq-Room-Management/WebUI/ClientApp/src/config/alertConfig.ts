import Swal, { SweetAlertOptions, SweetAlertResult } from 'sweetalert2';

export const alertCustom = (
  options: SweetAlertOptions
): Promise<SweetAlertResult> => {
  return Swal.fire({ ...options, heightAuto: false });
};


export const showValidationMessage = (message: string) => {
  Swal.showValidationMessage(message);
};

export const alertConfirm = (
  message: string,
  warning: string
): Promise<SweetAlertResult> => {
  return Swal.fire({
    html: `<div class="py-2">
						<h6 class='text-lg font-medium'>${message}</h6>
						<p class='text-sm text-gray-500'>${warning}</p>
					</div>
					`,
    icon: 'warning',
    showCancelButton: true,
    heightAuto: false,
    confirmButtonColor: '#54B8BD',
    cancelButtonColor: '#dc2626',
    confirmButtonText: 'نعم',
    cancelButtonText: 'لا'
  });
};
