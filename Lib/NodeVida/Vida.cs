using Godot;
using System;

/// <summary>
/// Componente que lida apenas com a vida da entidade
/// Nao toma decisoes pela entidade
/// Conectar um signal pois esse componente de vida nao precisa saber que classe concreta é o pai (jogador, inimigo, robo...)
/// </summary>
public partial class Vida : Node
{

    [Export]
    public float VidaMaxima = 100;

    [Export]
	public float VidaAtual = 0;

    [Signal]
    public delegate void MorteEventHandler();

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
		if (VidaAtual == 0){
            VidaAtual = VidaMaxima;
        }
    }


	public void AplicarDano(float valor){
        VidaAtual -= valor;

		if (VidaAtual <= 0) {
        	this.EmitSignalMorte();
		}
    }


	public void AplicarCura(float valor) {
        VidaAtual = Mathf.Max(VidaAtual + valor, VidaMaxima);
    }
}
