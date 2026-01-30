using Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using static Application.DTOs.BolaoDto;

namespace Application.Validators
{
    public class BolaoValidator: AbstractValidator<CriarBolaoDto>
    {
        public BolaoValidator()
        {            

            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome do bolão é obrigatório.")
                .Length(3, 100).WithMessage("O nome deve ter entre 3 e 100 caracteres.");
            RuleFor(x => x.Valor)
                .GreaterThanOrEqualTo(0).WithMessage("O valor da entrada não pode ser negativo.")
                .LessThan(10000).WithMessage("O valor máximo permitido por bolão é R$ 10.000,00.");           
            RuleFor(x => x.DtFechamento)
                .NotEmpty().WithMessage("A data de fechamento é obrigatória.")
                .GreaterThan(DateTime.Now.AddMinutes(30))
                .WithMessage("A data de fechamento deve ser pelo menos 30 minutos no futuro.");
            RuleFor(x => x.Visibilidade)
                .IsInEnum().WithMessage("O tipo de visibilidade informado é inválido.");

            RuleFor(x => x.TipoBolao)
                .IsInEnum().WithMessage("O tipo de bolão informado é inválido.");
        }
    }
}
